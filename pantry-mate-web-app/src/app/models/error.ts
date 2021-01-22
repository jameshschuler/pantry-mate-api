import { HttpErrorResponse } from "@angular/common/http";

export interface AppError {
    message: string;
}

export interface APIErrorResponse extends HttpErrorResponse {
    errors: any;
    status: number;
    title: string;
}