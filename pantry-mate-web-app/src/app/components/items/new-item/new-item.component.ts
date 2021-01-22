import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { EMPTY, Observable } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Brand } from 'src/app/models/brand';
import { AppError } from 'src/app/models/error';
import { ItemFormModel, NewItemRequest } from 'src/app/models/item';
import { AlertService } from 'src/app/services/alert.service';
import { BrandService } from 'src/app/services/brand.service';
import { ItemService } from 'src/app/services/item.service';

@Component( {
    selector: 'app-new-item',
    templateUrl: './new-item.component.html',
    styleUrls: ['./new-item.component.scss']
} )
export class NewItemComponent implements OnInit {

    public brands$: Observable<Brand[]> = EMPTY;

    public item = new ItemFormModel();
    public loading = false;
    public error: AppError | null = null;

    constructor ( private alertService: AlertService, private brandService: BrandService, private itemService: ItemService, private router: Router ) { }

    ngOnInit (): void {
        this.brands$ = this.brandService.getBrands()
            .pipe(
                tap( () => this.loading = false ),
                catchError( err => {
                    this.loading = false
                    this.alertService.error( err, { autoClose: false } );
                    return EMPTY;
                } )
            );
    }

    onSubmit ( newItemForm: NgForm ) {
        const { name, price, description, uom, brand } = newItemForm.value;
        const request = { name, price, description, unitOfMeasureId: uom, brandId: brand } as NewItemRequest;
        this.itemService
            .createItem( request )
            .subscribe(
                ( data ) => {
                    console.log( 'data', data );
                    this.router.navigate( ['/items'] );
                },
                error => {
                    this.alertService.error( error, { autoClose: false } );
                    this.loading = false;
                } );
    }

}
