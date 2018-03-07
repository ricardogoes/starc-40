import { RouterModule, Routes, provideRoutes } from '@angular/router';

import { AuthGuard } from './_shared/services/authentication/authentication.guard';

import { PublicComponent} from './layouts/public/public.component';
import { SecureComponent} from './layouts/secure/secure.component';

import { SECURE_ROUTES } from './secure/secure.routes';
import { PUBLIC_ROUTES } from './public/public.routes';

export const APP_ROUTES: Routes = [
    { path: '', redirectTo: '/login', pathMatch: 'full', },
    { path: '', component: PublicComponent, data: { title: 'Public Views' }, children: PUBLIC_ROUTES },
    { path: '', component: SecureComponent, canActivate: [AuthGuard], data: { title: 'Secure Views' }, children: SECURE_ROUTES }
];  

export const AppRoutes = RouterModule.forRoot(APP_ROUTES);