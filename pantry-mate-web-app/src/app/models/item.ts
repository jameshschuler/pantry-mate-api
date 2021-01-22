export class ItemFormModel {
    constructor ( public name = '', public price = '' ) {

    }
}

export interface Item {
    abbreviatedUnitOfMeasure: string;
    brand: string;
    description: string;
    itemId: number;
    name: string;
    price: number;
    unitOfMeasure: string;
}

export interface NewItemRequest {
    name: string;
    price: number;
    description: string;
    unitOfMeasureId?: number;
    brandId?: number;
}