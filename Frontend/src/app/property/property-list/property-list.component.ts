import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HousingService } from 'src/app/services/housing.service';
import { IPropertyBase } from 'src/app/model/ipropertybase';

@Component({
  selector: 'app-property-list',
  templateUrl: './property-list.component.html',
  styleUrls: ['./property-list.component.css']
})

export class PropertyListComponent implements OnInit {
  SellOrRent = 1;
  properties: Array<IPropertyBase>;
  today = new Date();
  city = "";
  searchCity = "";
  sortByParam = "";
  sortDirection = "desc";

  constructor(private route: ActivatedRoute, private housingService: HousingService) {
    this.properties = [];
  }

  ngOnInit(): void {
    if (this.route.snapshot.url.toString()) {
      this.SellOrRent = 2;
    }
    this.housingService.getAllProperties(this.SellOrRent).subscribe(
      data=>{
        this.properties = data;
        console.log(data);
      }, error => {
        console.log("httperror:")
        console.log(error);
      }
    );
  }

  onCityFilter() {
    this.searchCity = this.city;
  }

  onCityFilterClear() {
    this.searchCity = "";
    this.city = "";
  }

  onSortDirection() {
    if (this.sortDirection === "desc") {
      this.sortDirection = "asc";
    } else {
      this.sortDirection = "desc";
    }
  }
}
