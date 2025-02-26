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

import {ModuleRegistryExtend} from "cs2/modding";
import React, {useEffect, useState} from "react";
import {Icon, PanelSection, Tooltip} from "cs2/ui";
import {useValue} from "cs2/api";
import {CrimeRemover} from "./base";
import {useLocalization} from "cs2/l10n";
import getTranslationKeyOf = CrimeRemover.getTranslationKeyOf;
import selectedEntity$ = CrimeRemover.selectedEntity$;
import isCriminal$ = CrimeRemover.isCriminal$;
import markCriminal = CrimeRemover.markCriminal;
import removeCriminal = CrimeRemover.removeCriminal;
import isShowCitizenPanel$ = CrimeRemover.isShowCitizenPanel$;

export const MarkCriminalSectionKey = "CrimeRemover.MarkCriminal"

const MarkCriminalComponent = () => {
    const isCriminal = useValue(isCriminal$)
    const showCitizenPanel = useValue(isShowCitizenPanel$)
    const selectedEntity = useValue(selectedEntity$)

    const {translate} = useLocalization()

    const buttonClass = "button_Z9O button_ECf item_It6 item-mouse-states_Fmi " +
        "item-selected_tAM item-focused_FuT button_Z9O button_ECf item_It6 " +
        "item-mouse-states_Fmi item-selected_tAM item-focused_FuT button_xGY"

    const selectedButtonClass = buttonClass + " selected";

    const [markCriminalClass, setMarkCriminalClass] = useState(selectedButtonClass)
    useEffect(() => {
        if (isCriminal) {
            setMarkCriminalClass(selectedButtonClass)
        } else {
            setMarkCriminalClass(buttonClass)
        }
    }, [isCriminal])

    const [markCriminalKey, setMarkCriminalKey] = useState(getTranslationKeyOf("MarkCriminal"))
    useEffect(() => {
        if (isCriminal) {
            setMarkCriminalKey(getTranslationKeyOf("RemoveCriminal"))
        } else {
            setMarkCriminalKey(getTranslationKeyOf("MarkCriminal"))
        }
    }, [isCriminal])

    return showCitizenPanel ? (<PanelSection>
        <div className="actions-section_X1x info-row_QQ9">
            <div className="left_RyE uppercase_f0y">
                {translate(getTranslationKeyOf("CRIME_ADJUSTER"), "Crime Adjuster")}
            </div>
            <Tooltip tooltip={
                <div>
                    <div className="title_lCJ">{
                        translate(markCriminalKey,
                            isCriminal ? "Remove Criminal" : "Mark Criminal")
                    }</div>
                    <div>{
                        translate(markCriminalKey + ".DESC",
                            isCriminal ? "Remove Criminal" : "Mark Criminal")
                    }</div>
                </div>
            }>
                <div onClick={() => {
                    if (isCriminal) {
                        removeCriminal(selectedEntity)
                    } else {
                        markCriminal(selectedEntity)
                    }
                }} className={markCriminalClass}>
                    {isCriminal
                        ? <Icon src="Media/Game/Icons/Unfollow.svg"/>
                        : <Icon src="Media/Game/Icons/Follow.svg"/>
                    }
                </div>
            </Tooltip>
        </div>
    </PanelSection>) : <div></div>
    // <Tooltip tooltip={
    //     <div>
    //         <div>Call Police</div>
    //     </div>
    // }>
    //     <div onClick={() => {
    //         callPolice(selectedEntity)
    //     }} className={buttonClass}>
    //
    //         <Icon
    //             src="Media/Game/Icons/Police.svg"
    //         />
    //     </div>
    // </Tooltip>
}

export const InfoPanelMarkCriminalComponent: ModuleRegistryExtend = (components: any): any => {
    components[MarkCriminalSectionKey] = () =>
        <MarkCriminalComponent/>

    return components as any
}
