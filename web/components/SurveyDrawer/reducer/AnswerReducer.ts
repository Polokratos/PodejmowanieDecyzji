import { Answer, Question } from "../../../types/types"


export type AnswerAction = {
    id : number
    answer : Answer
}

export const answerReducer = (state:Question[], action:AnswerAction) => state.map(q => {
    return q.id === action.id ? {...q, answer:action.answer} : q;
});