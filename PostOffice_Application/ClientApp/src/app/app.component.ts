import { Component, OnInit } from '@angular/core';
import { DataService } from './data.service';
import { Parcel } from './parcel';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    providers: [DataService]
})
export class AppComponent implements OnInit {

    status: string = "";
    parcel: Parcel = new Parcel();   // изменяемая посылка
    parcels: Parcel[];                // массив посылок
    tableMode: boolean = true;          // табличный режим

    constructor(private dataService: DataService) { }

    // загрузка данных при старте компонента
    ngOnInit() {
        this.loadParcels();
    }
    // получаем данные через сервис
    loadParcels() {
        this.dataService.getParcels()
            .subscribe((data: Parcel[]) => this.parcels = data);
    }
    // сохранение данных
    save() {
        if (this.parcel.id == null) {
            this.dataService.createParcel(this.parcel)
                .subscribe((data: Parcel) => this.parcels.push(data));
        } else {
            this.dataService.updateParcel(this.parcel)
                .subscribe(data => this.loadParcels());
        }
        this.cancel();
    }
    // редактирование
    editParcel(p: Parcel) {
        this.parcel = p;
    }
    // отмена
    cancel() {
        this.parcel = new Parcel();
        this.tableMode = true;
    }
    // удаление
    delete(p: Parcel) {
        this.dataService.deleteParcel(p.id)
            .subscribe(data => this.loadParcels());
    }
    // добавление
    add() {
        this.cancel();
        this.tableMode = false;
    }
    // фильтрация записей
    selectChangedHandler(event: any) {
        this.status = event.target.value;
        let statusString: string = "";
        switch (this.status) {
            case "sorting":
                statusString = "В сортировочном центре";
                break;
            case "sent":
                statusString = "Отправлено";
                break;
            case "delivered":
                statusString = "Доставлено";
                break;
            case "received":
                statusString = "Получено";
                break;
            case "all":
                statusString = "";
                break;
        }
        console.log(this.status);
        let parcelRows = Array.from(document.querySelectorAll(".parcelRow"));

        parcelRows.forEach(item => {
            console.log(item.children[3].innerHTML);
            if (item.children[3].innerHTML == statusString || statusString == "") {
                let HTMLItem = item as HTMLElement;
                HTMLItem.style.display = "table-row";
            } else {
                let HTMLItem = item as HTMLElement;
                HTMLItem.style.display = "none";
            }
        })
    }
}