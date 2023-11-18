
export type Question = {
    id : number
    context? : SurveyField
    option1 : SurveyField
    option2 : SurveyField
    answer? : Answer
}

export type Answer = string


export type SurveyHeader = {
    id: number
    name: string
}
export const TestSurveyHeader:SurveyHeader = {
    id:-1,
    name:"Test survey header"
}

export type SurveyDetails = {
    alternatives : Question[]
    criteria : Question[]
    context : SurveyField
}
export const TestSurveyDetails:SurveyDetails = {
    context : "Test survey context",
    alternatives:[
        {
            id:-2,
            option1 : "alt1",
            option2 : "alt2",
        },
        {
            id:-3,
            option1 : "alt3",
            option2 : "alt4",
            context : "altContext"
        },
        {
            id:-4,
            option1 : "alt5",
            option2 : "alt6",
        },
    ],
    criteria:[
        {
            id:-5,
            option1 : "cri1",
            option2 : "cri2",
        },
        {
            id:-6,
            option1 : "cri3",
            option2 : "cri4",
            context : "criContext"
        },
    ]
}

export type Survey = {
    id: number
    name: string
    context? : SurveyField
    alternatives : Question[]
    criteria : Question[]
}

export type SurveyField = string | JSX.Element;

//deprecated to get compliance to API
const TestSurvey:Survey = {
    id:-1,
    name:"TEST SURVEY",
    context:"TEST SURVEY CONTEXT",
    alternatives:[
        {
            id:-2,
            option1 : "alt1",
            option2 : "alt2",
        },
        {
            id:-3,
            option1 : "alt3",
            option2 : "alt4",
            context : "altContext"
        },
        {
            id:-4,
            option1 : "alt5",
            option2 : "alt6",
        },
    ],
    criteria:[
        {
            id:-5,
            option1 : "cri1",
            option2 : "cri2",
        },
        {
            id:-6,
            option1 : "cri3",
            option2 : "cri4",
            context : "criContext"
        },
    ]
}