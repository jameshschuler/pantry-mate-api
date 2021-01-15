import { Component, OnInit } from '@angular/core';
import { EMPTY, Observable } from 'rxjs';
import { of } from 'rxjs/internal/observable/of';
import { catchError, first, map, take, tap } from 'rxjs/operators';
import { Item } from 'src/app/models/item';
import { AlertService } from 'src/app/services/alert.service';
import { ItemService } from 'src/app/services/item.service';

@Component( {
    selector: 'app-items',
    templateUrl: './items.component.html',
    styleUrls: ['./items.component.scss']
} )
export class ItemsComponent implements OnInit {

    public items$: Observable<Item[]> = EMPTY;
    public loading = true;

    constructor ( private alertService: AlertService, private itemService: ItemService ) { }

    ngOnInit (): void {
        this.items$ = this.itemService.getItems()
            .pipe(
                tap( () => this.loading = false ),
                catchError( err => {
                    this.loading = false
                    this.alertService.error( err, { autoClose: false } );
                    return EMPTY;
                } )
            );
    }
}
