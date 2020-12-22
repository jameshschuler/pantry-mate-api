import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { Router, NavigationStart } from '@angular/router';
import { Subscription } from 'rxjs';

import { Alert, AlertType } from '../../models/alert';
import { AlertService } from '../../services/alert.service';

@Component( { selector: 'alert', templateUrl: 'alert.component.html', styleUrls: ['./alert.component.scss'] } )
export class AlertComponent implements OnInit, OnDestroy {
    @Input() id = 'default-alert';
    @Input() fade = true;

    alerts: Alert[] = [];
    alertSubscription: Subscription | null = null;
    routeSubscription: Subscription | null = null;

    constructor ( private router: Router, private alertService: AlertService ) { }

    ngOnInit () {
        this.alertSubscription = this.alertService.onAlert( this.id )
            .subscribe( alert => {
                if ( !alert.message ) {
                    this.alerts = this.alerts.filter( f => f.keepAfterRouteChange );

                    this.alerts.forEach( f => delete f.keepAfterRouteChange );
                    return;
                }

                this.alerts.push( alert );

                if ( alert.autoClose ) {
                    setTimeout( () => this.removeAlert( alert ), 3000 );
                }
            } );

        this.routeSubscription = this.router.events.subscribe( event => {
            if ( event instanceof NavigationStart ) {
                this.alertService.clear( this.id );
            }
        } );
    }

    ngOnDestroy () {
        this.alertSubscription!.unsubscribe();
        this.routeSubscription!.unsubscribe();
    }

    removeAlert ( alert: Alert ) {
        if ( !this.alerts.includes( alert ) ) {
            return;
        }

        if ( this.fade ) {
            this.alerts.find( f => f === alert )!.fade = true;

            setTimeout( () => {
                this.alerts = this.alerts.filter( f => f !== alert );
            }, 250 );
        } else {
            // remove alert
            this.alerts = this.alerts.filter( f => f !== alert );
        }
    }

    cssClass ( alert: Alert ) {
        if ( !alert ) {
            return;
        }

        const classes = ['alert', 'alert-dismissable', 'mt-4', 'container', 'show'];

        const alertTypeClass = {
            [AlertType.Success]: 'alert alert-success',
            [AlertType.Error]: 'alert alert-danger',
            [AlertType.Info]: 'alert alert-info',
            [AlertType.Warning]: 'alert alert-warning'
        }

        classes.push( alertTypeClass[alert.type] );

        if ( alert.fade ) {
            classes.push( 'fade' );
        }

        return classes.join( ' ' );
    }
}