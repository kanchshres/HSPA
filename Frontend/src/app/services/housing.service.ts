import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { Property } from 'src/app/model/property';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})

export class HousingService {

  baseUrl = environment.baseUrl;
  constructor(private http: HttpClient) { }

  getAllCities(): Observable<string[]> {
    return this.http.get<string[]>(this.baseUrl + '/city/cities');
  }

  getProperty(ID: number) {
    return this.http.get<Property>(this.baseUrl + "/property/detail/" + ID.toString());
  }

  getAllProperties(SellOrRent?: number): Observable<Property[]> {
    return this.http.get<Property[]>(this.baseUrl + "/property/list/" + SellOrRent.toString());
  }

  addProperty(property: Property) {
    let newProp = [property];

    if (localStorage.getItem('newProp')) {
      newProp = [property, ...JSON.parse(localStorage.getItem('newProp'))];
    }
    localStorage.setItem('newProp', JSON.stringify(newProp));
  }

  newPropID() {
    if (localStorage.getItem('PID')) {
      localStorage.setItem('PID', String(+localStorage.getItem('PID')) + 1);
      return +localStorage.getItem('PID');
    } else {
      localStorage.setItem('PID', '101');
      return 101;
    }
  }

  getPropertyAge(dateOfEstablishment: Date): string
  {
    const today = new Date();
    const estDate = new Date(dateOfEstablishment);
    let age = today.getFullYear() - estDate.getFullYear();
    const m = today.getMonth() - estDate.getMonth();

    // Current month smaller than establishment month or
    // Same month but current date smalelr than establishment date
    if (m < 0 || (m === 0 && today.getDate() < estDate.getDate())) {
      age --;
    }

    // Establishment date is future date
    if (today < estDate) {
      return '0';
    }

    // Age is less than a year
    if (age === 0) {
      return 'Less than a year';
    }

    return age.toString();
  }
}
