import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot } from '@angular/router';
import { Property } from '../model/property';
import { Observable, catchError, of } from 'rxjs';
import { HousingService } from './housing.service';

@Injectable({
  providedIn: 'root'
})
export class PropertyDetailResolverService implements Resolve<Property>{

constructor(private housingService: HousingService,
            private router: Router) { }

resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):
  Observable<Property>|Property {

    const propID = route.params['id'];
    return this.housingService.getProperty(+propID).pipe(
      catchError(error => {
        this.router.navigate(['/']);
        return of(null);
      })
  );
  }
}
