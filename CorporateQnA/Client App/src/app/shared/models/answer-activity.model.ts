import { Activity } from './activity.model.ts';

export class AnswerActivity {
    id?:number;
    activityPerformedOn:number;
    activityBy:number;
    type:Activity;
    activityOn:Date;
}
