/**
 * Copyright (c) 2024 RollW
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

const path = require("path");
const MOD = require("./mod.json");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const { CSSPresencePlugin } = require("./tools/css-presence");
const TerserPlugin = require("terser-webpack-plugin");
const gray = (text) => `\x1b[90m${text}\x1b[0m`;

const CSII_USERDATAPATH = process.env.CSII_USERDATAPATH;

if (!CSII_USERDATAPATH) {
  throw "CSII_USERDATAPATH environment variable is not set, ensure the CSII Modding Toolchain is installed correctly";
}

const OUTPUT_DIR = `${CSII_USERDATAPATH}\\Mods\\${MOD.id}`;

const banner = `
 * Cities: Skylines II UI Module
 *
 * Id: ${MOD.id}
 * Author: ${MOD.author}
 * Version: ${MOD.version}
 * Dependencies: ${MOD.dependencies.join(",")}
`;

module.exports = {
  mode: "production",
  stats: "none",
  entry: {
    [MOD.id]: "./src/index.tsx",
  },
  externalsType: "window",
  externals: {
    react: "React",
    "react-dom": "ReactDOM",
    "cs2/modding": "cs2/modding",
    "cs2/api": "cs2/api",
    "cs2/bindings": "cs2/bindings",
    "cs2/l10n": "cs2/l10n",
    "cs2/ui": "cs2/ui",
    "cs2/input": "cs2/input",
    "cs2/utils": "cs2/utils",
    "cohtml/cohtml": "cohtml/cohtml",
  },
  module: {
    rules: [
      {
        test: /\.tsx?$/,
        use: "ts-loader",
        exclude: /node_modules/,
      },
      {
        test: /\.s?css$/,
        include: path.join(__dirname, "src"),
        use: [
          MiniCssExtractPlugin.loader,
          {
            loader: "css-loader",
            options: {
              url: true,
              importLoaders: 1,
              modules: {
                auto: true,
                exportLocalsConvention: "camelCase",
                localIdentName: "[local]_[hash:base64:3]",
              },
            },
          },
          "sass-loader",
        ],
      },
      {
        test: /\.(png|jpe?g|gif|svg)$/i,
        type: "asset/resource",
        generator: {
          filename: "images/[name][ext][query]",
        },
      },
    ],
  },
  resolve: {
    extensions: [".tsx", ".ts", ".js"],
    modules: ["node_modules", path.join(__dirname, "src")],
    alias: {
      "mod.json": path.resolve(__dirname, "mod.json"),
    },
  },
  output: {
    path: path.resolve(__dirname, OUTPUT_DIR),
    library: {
      type: "module",
    },
    publicPath: `coui://ui-mods/`,
  },
  optimization: {
    minimize: true,
    minimizer: [
      new TerserPlugin({
        extractComments: {
          banner: () => banner,
        },
      }),
    ],
  },
  experiments: {
    outputModule: true,
  },
  plugins: [
    new MiniCssExtractPlugin(),
    new CSSPresencePlugin(),
    {
      apply(compiler) {
        let runCount = 0;
        compiler.hooks.done.tap("AfterDonePlugin", (stats) => {
          console.log(stats.toString({ colors: true }));
          console.log(`\n🔨 ${!runCount++ ? "Built" : "Updated"} ${MOD.id}`);
          console.log("   " + gray(OUTPUT_DIR) + "\n");
        });
      },
    },
  ],
};
