import { Component, OnInit } from '@angular/core';
import { HealthCheck } from './models/healthcheck';
import { HealthCheckService } from './services/healthcheck.service';

@Component( {
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
} )
export class AppComponent implements OnInit {
    title = 'pantry-mate';

    constructor ( private healthCheckService: HealthCheckService, ) {

    }

    ngOnInit (): void {
        this.healthCheckService.getAPIHealth().subscribe( ( result: HealthCheck ) => {
            console.log( result );
        } );
    }
}
