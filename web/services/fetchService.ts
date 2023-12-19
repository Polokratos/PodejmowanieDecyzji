const SERVER_URL = "http://localhost:8080";
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
    alternatives?: CriterionDTO[],
    criteria?: CriterionDTO[],
    results?: RankingDTO[]
  }


const sendRequest = <RsType>(endpoint:string,body: any) : Promise<RsType> => {
    return fetch(
        endpoint,
        {
            method: "POST",
            body : JSON.stringify(body),
            headers : {'Content-Type' : "application/json"}
        }).then(response => {
            if(!response.ok){
                return Promise.reject();
            }
            return response.json() as Promise<RsType>;
        })
}

const login = (body:UserLoginDTO) => sendRequest<SessionToken>(LOGIN_ENDPOINT,body);
const getHeaders = (body:SessionToken) => sendRequest<RankingHeaderDTO[]>(HEADER_ENDPOINT,body);
const getSurvey = (id:number,body:SessionToken) => sendRequest<RankingDTO>(SURVEY_ENDPOINTS+id.toString(),body);
const submitAnswer = (body:RankingAnswerDTO) => sendRequest<void>(SUBMIT_ENDPOINT,body);

export const fetchService = {login,getHeaders,getSurvey,submitAnswer}