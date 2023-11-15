import { useState } from "react"
import { Answer, Question, SurveyField } from "../../types/types"

export class SurveyBodyProps {
    question: Question
    surveyContext : SurveyField
    onPrev : (answer:Answer) => void
    onNext : (answer:Answer) => void
    initialAnswer:Answer
}

export const SurveyBodyComponent = (props : SurveyBodyProps) : JSX.Element => {
    
    const {question,surveyContext,onNext,onPrev,initialAnswer} = props;

    const [answer,setAnswer] = useState(initialAnswer);

    return (
        <div className="drawer-body">   
            <div className="drawer-body-row-center">
                <div className="drawer-context">{question.context ?? surveyContext ?? ""}</div>
            </div>

            <div className="drawer-body-row-center">
                <div className="drawer-body-option">{question.option1}</div>
                <div className="drawer-body-option">{question.option2}</div>
            </div>
            
            <div className="drawer-body-row-sb">
                <button className="drawer-body-button" onClick={()=>{onPrev(answer); setAnswer(initialAnswer);}}>{"Prev"}</button>
                <div className="drawer-input-container">
                    <a> input context</a>
                    <input value={answer} onChange={(e) => setAnswer(e.target.value)}/>
                </div>
                <button className="drawer-body-button" onClick={() => {onNext(answer); setAnswer(initialAnswer);}}>{"Next"}</button>
            </div>
        </div>
    )
}