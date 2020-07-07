import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Parcel } from './parcel';

@Injectable()
export class DataService {

    private url = "/api/parcels";

    constructor(private http: HttpClient) {
    }

    getParcels() {
        return this.http.get(this.url);
    }

    getParcel(id: number) {
        return this.http.get(this.url + '/' + id);
    }

    createParcel(parcel: Parcel) {
        return this.http.post(this.url, parcel);
    }

    updateParcel(parcel: Parcel) {

        return this.http.put(this.url, parcel);
    }

    deleteParcel(id: number) {
        return this.http.delete(this.url + '/' + id);
    }
}