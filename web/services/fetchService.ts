import { SurveyHeader } from "../types/types";

const SERVER_URL = "http://localhost:5000";
const LOGIN_ENDPOINT = SERVER_URL +  "/login";
const HEADER_ENDPOINT = SERVER_URL + "/headers";
const SURVEY_ENDPOINTS = SERVER_URL + "/survey";
const CREATE_ENDPOINT = SERVER_URL + "/create";
const SUBMIT_ENDPOINT = SERVER_URL + "/submit";

export type SessionToken = string;
export type UserLoginDTO = {username:string, password:string}
export type RankingAnswerDTO = {
    criterionID: number,
    leftAlternativeID: number,
    rightAlternativeID: number,
    value: number,
}
export type RankingPostDTO = {
    rankingID: number,
    answers: RankingAnswerDTO[]
}
export type RankingHeaderDTO = {id:number,name?:string,description?:string}
export type ScaleValueDTO = {value: number,description: string}
export type AlternativeDTO = {alternativeId: number,name: string,description: string}
export type CriterionDTO = {criterionId: number,name: string,description: string}
export type ResultDTO = {rankingId: number,criterionId: number,alternativeId: number}

export type RankingDTO = {
    rankingId: number,
    name?: string,
    description?: string,
    calculationMethod: number,
    aggregationMethod: number,
    isComplete: boolean,
    askOrder?: string,
    creationDate?: string,
    endDate: string,
    scale?: ScaleValueDTO[],
    alternatives?: AlternativeDTO[],
    criteria?: CriterionDTO[],
    results?: RankingDTO[]
  }


const sendRequest = <Rstype>(endpoint:string,body:any,isText:boolean=false) : Promise<Rstype> => {
    return fetch(
        endpoint,
        {
            method: "POST",
            body : JSON.stringify(body),
            headers : {
                'Content-export type' : "application/json",
            }
        }).then(response => {
            if(!response.ok){
                return Promise.reject();
            }
            if(isText)
            {
                return response.text() as Promise<Rstype>;
            }
            return response.json() as Promise<Rstype>;
        })
}

const login = (body:UserLoginDTO) => sendRequest<SessionToken>(LOGIN_ENDPOINT,body);
const getHeaders = () => sendRequest<RankingHeaderDTO[]>(HEADER_ENDPOINT,stok());
const getSurvey = (id:number) => sendRequest<RankingDTO>(SURVEY_ENDPOINTS+id.toString(),stok());
const submitAnswer = (body:RankingPostDTO) => sendRequest<void>(SUBMIT_ENDPOINT,body);
const stok = () => window.sessionStorage.getItem("sessionKey");
export const fetchService = {login,getHeaders,getSurvey,submitAnswer}