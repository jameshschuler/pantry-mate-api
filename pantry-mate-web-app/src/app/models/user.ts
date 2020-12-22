export class UserFormModel {
    constructor ( public username = '', public password = '' ) {

    }
}

export interface User {
    accountId: number;
    token: string;
    username: string;
}