import { Activity } from './activity.model.ts';

export class QuestionActivity {
    id?:number;
    activityPerformedOn:number;
    activityBy:number;
    type:Activity;
    activityOn:Date;
}

