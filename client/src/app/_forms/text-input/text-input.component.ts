import { Component, Input, OnInit, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl } from '@angular/forms';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.css']
})
export class TextInputComponent implements  ControlValueAccessor {
  @Input() label = '';
  @Input() type = 'text';

  constructor(@Self()  public ngControl: NgControl ) {
    // control the value of the text value and assign it
    this.ngControl.valueAccessor =  this;
   }
  writeValue(obj: any): void {

  }
  registerOnChange(fn: any): void {

  }
  registerOnTouched(fn: any): void {

  }
  setDisabledState?(isDisabled: boolean): void {
    //throw new Error('Method not implemented.');
  }

  // if you have error null error on your html tag like this then enable this method [formControl]="ngControl.control"

  // get control() : FormControl{

  //   return this.ngControl.control as FormControl;
  // }

}
