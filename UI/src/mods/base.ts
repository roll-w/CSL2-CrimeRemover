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

import {Entity} from "cs2/input";
import {bindValue, trigger, useValue} from "cs2/api";

export namespace CrimeRemover {
    export const ModName = "RollW_CrimeRemover";

    export const selectedEntity$ = bindValue<Entity>(
        ModName, "selectedEntity", {index: 0, version: 0}
    );

    export const markCriminal = (entity: Entity) => {
        trigger(ModName, "markCriminal", entity);
    }

    export const removeCriminal = (entity: Entity) => {
        trigger(ModName, "removeCriminal", entity);
    }

    export const isCriminal$ = bindValue<boolean>(
        ModName, "isCriminal", false
    )

    export const isShowCitizenPanel$ = bindValue<boolean>(
        ModName, "isShowCitizenPanel", false
    )

    export const isShowCitizenPanel = () => {
        return useValue(isShowCitizenPanel$);
    }

    export const callPolice = (entity: Entity) => {
        trigger(ModName, "callPolice", entity);
    }

    export type BaseSerialized<T> = {
        value__: T;
        __Type: string;
    }

    export type SerializedEntity = {
        Index: number;
        Version: number;
        __Type: string;
    }

    export const toEntity = (entity: SerializedEntity): Entity => {
        return {
            index: entity.Index,
            version: entity.Version
        }
    }

    export const getTranslationKeyOf = (key: string, type = '') => {
        if (!type || type === '') {
            return `CrimeRemover.${key}`;
        }

        return `CrimeRemover.${type}[${key}]`;
    }
}