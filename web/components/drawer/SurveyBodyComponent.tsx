import { useState } from "react"

export class SurveyBodyComponentProps {
    OptionOne : string | JSX.Element
    OptionTwo : string | JSX.Element
    Context?  : string | JSX.Element = "Context"
    getNext?  : () => SurveyBodyComponentProps
    getPrev?  : () => SurveyBodyComponentProps 
}

export const SurveyBodyComponent = (props : SurveyBodyComponentProps) : JSX.Element => {
    
    const [optionOne, setOptionOne] = useState(props.OptionOne)
    const [optionTwo, setOptionTwo] = useState(props.OptionTwo)
    const [context, setContext] = useState(props.Context)

    return (
        <div className="drawer-body">   
            <div className="drawer-body-row-center">
                <div className="drawer-context">{context}</div>
            </div>

            <div className="drawer-body-row-center">
                <div className="drawer-body-option">{optionOne}</div>
                <div className="drawer-body-option">{optionTwo}</div>
            </div>
            
            <div className="drawer-body-row-sb">
                <button className="drawer-body-button">{"<<<"}</button>
                <div className="drawer-input-container">
                    <a> input context</a>
                    <input value={"Input placeholder"}></input>
                </div>
                <button className="drawer-body-button">{">>>"}</button>
            </div>
        </div>
    )
}