import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { PaginationComponent } from './pagination.component';

@NgModule({
  imports: [
    BrowserModule
  ],
  declarations: [ PaginationComponent ],
  exports: [ PaginationComponent ]
})
export class DataGridModule { }
