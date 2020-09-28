import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AlertifyService } from 'src/app/services/alertify.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  submitted = false;

  constructor(private formBuilder: FormBuilder,
    private router: Router,
    private authService: AuthService,
    private alertifyService : AlertifyService) { }

  loginForm: FormGroup;

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  onSubmit() {    
    this.submitted = true;
    if (this.loginForm.valid) {
      this.authService.login(this.loginForm.value)
        .subscribe(data => {        
          this.onSaveComplete(data);
        },
          error => { 
            this.onError(error);
        });
    }
  }

  async onSaveComplete(response: any) {
    this.loginForm.reset();
    let res = await response; 
    localStorage.setItem('token', res.token);
    localStorage.setItem('user', JSON.stringify(res.userName)); 
    this.alertifyService.success('You are logged in!');
    this.router.navigate(['']);
  }

  onError(error: any) { 
    this.alertifyService.error(error);
  }
}
