import { useState } from "react"
import { Answer, Question, SurveyField } from "../../types/types"

export type SurveyBodyProps = {
    currentQuestion: Question
    surveyContext : SurveyField
    previous : () => void
    next : () => void
    answer: Answer
    setAnswer: (answer: Answer) => void
}

export const mapAnswerToServer = (answer:number) => {
    if(answer == 0) return 0;
    if(answer > 0) return answer +1;
    return 1/(-answer+1);
}

export const SurveyBodyComponent = (props : SurveyBodyProps) : JSX.Element => {
    
    const {currentQuestion: question,surveyContext,next,previous,answer,setAnswer} = props;

    const mapAnswerToUi = (answer:number) => Math.abs(answer)+1;

    const QuestionContainer = (
        <>
            <div className="drawer-body-row-center">
                <div className="drawer-context">{question?.context ?? surveyContext ?? ""}</div>
            </div>
            <div className="drawer-body-row-center">
                <div className="drawer-body-option">{question.option1}</div>
                <div className="drawer-body-option">{question.option2}</div>
            </div>
        </>
    );

    const AnswerContainer = (
        <div className="drawer-body-row-sb">
            <button className="drawer-body-button" onClick={previous}>{"Prev"}</button>
            <div className="drawer-input-container">
                <div className="drawer-body-row-sb">
                    <a>9x</a><a>7x</a><a>5x</a><a>3x</a>
                    <a>{mapAnswerToUi(answer)}</a>
                    <a>3x</a><a>5x</a><a>7x</a><a>9x</a>
                </div>
                <input type="range" min="-8" max="8" value={answer} onChange={(e) => {setAnswer(Number.parseFloat(e.target.value));}}/>
            </div>
            <button className="drawer-body-button" onClick={next}>{"Next"}</button>
        </div>
    )


    return (
        <div className="drawer-body">   
            {QuestionContainer}
            {AnswerContainer}
        </div>
    )
}