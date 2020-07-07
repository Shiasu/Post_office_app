
export class Parcel {
    constructor(
        public id?: number,
        public parcelNumber?: number,
        public addresser?: string,
        public addressee?: string,
        public status?: number,
        public date?: Date) { }
}