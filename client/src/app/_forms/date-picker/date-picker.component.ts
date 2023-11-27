import { Component, Input, OnInit, Self } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';

@Component({
  selector: 'app-date-picker',
  templateUrl: './date-picker.component.html',
  styleUrls: ['./date-picker.component.css']
})
export class DatePickerComponent implements ControlValueAccessor {
  @Input() label = '';
  // @Input() type = 'text';
  @Input() maxDate : Date | undefined;
  bsConfig:  Partial<BsDatepickerConfig> | undefined;


  constructor(@Self()  public ngControl: NgControl ) {
    // control the value of the text value and assign it
    this.ngControl.valueAccessor =  this;
    this.bsConfig= {
      containerClass: 'theme-red',
      dateInputFormat: 'DD MMM YYYY'
    }
   }
  writeValue(obj: any): void {

  }
  registerOnChange(fn: any): void {

  }
  registerOnTouched(fn: any): void {

  }
  setDisabledState?(isDisabled: boolean): void {

  }



}
