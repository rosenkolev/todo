import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { HeaderComponent } from './components/header/header.component';
import { MenuComponent } from './components/menu/menu.component';
import { LayoutComponent } from './components/layout/layout.component';
import { LayoutService } from './layout.service';

const COMPONENTS = [
  HeaderComponent,
  MenuComponent,
  LayoutComponent
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule
  ],
  providers: [LayoutService],
  declarations: COMPONENTS,
  exports: COMPONENTS
})
export class LayoutModule {
}
