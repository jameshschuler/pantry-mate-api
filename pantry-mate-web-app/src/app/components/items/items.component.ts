import { Component, OnInit } from '@angular/core';
import { Item } from 'src/app/models/item';

@Component( {
    selector: 'app-items',
    templateUrl: './items.component.html',
    styleUrls: ['./items.component.scss']
} )
export class ItemsComponent implements OnInit {

    public items: Item[] = [];

    constructor () { }

    ngOnInit (): void {
    }

}
