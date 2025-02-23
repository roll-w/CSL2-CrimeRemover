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

import {selectedInfo} from "cs2/bindings";
import {MarkCriminalSectionKey} from "./MarkCriminalComponent";
import {ModuleRegistryAppend} from "cs2/modding";

let currentEntityIndex: number = 0;
const selectedEntity$ = selectedInfo.selectedEntity$;
const middleSections$ = selectedInfo.middleSections$;

export const InfoPanelBinding: ModuleRegistryAppend = () => {
    selectedEntity$.subscribe((entity) => {
        if (!entity.index) {
            currentEntityIndex = 0
            return entity
        }
        if (currentEntityIndex != entity.index) {
            currentEntityIndex = entity.index
        }
        return entity
    })

    middleSections$.subscribe(value => {
        if (currentEntityIndex && value.every(item =>
            item?.__Type !== MarkCriminalSectionKey as any)) {
            value.unshift({
                __Type: MarkCriminalSectionKey,
                group: "DescriptionSection"
            } as any)

            const desc = value[1]
            value[1] = value[0]
            value[0] = desc
        }
    })
    return <></>
}
