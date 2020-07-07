import { Pipe, PipeTransform } from '@angular/core'

@Pipe({
    name: 'statuses'
})
export class StatusesPipe implements PipeTransform {
    transform(value: number, args?: any): string {
        switch (value) {
            case 0:
                return "В сортировочном центре";
            case 1:
                return "Отправлено";
            case 2:
                return "Доставлено";
            case 3:
                return "Получено";
        }
    }
}