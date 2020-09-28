import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FileUploader } from 'ng2-file-upload';
import { NgxSpinnerService } from 'ngx-spinner';
import { ImageHelper } from 'src/app/helpers/image.helper';
import { FriendModel } from 'src/app/models/friend.model';
import { AlertifyService } from 'src/app/services/alertify.service';
import { FriendService } from 'src/app/services/friend.service';

@Component({
  selector: 'app-add-friend',
  templateUrl: './add-friend.component.html',
  styleUrls: ['./add-friend.component.css']
})
export class AddFriendComponent implements OnInit {

  @ViewChild('fileUploadInput') fileUploadInput: ElementRef;
  @ViewChild('fakeFileUploadInput') fakeFileUploadInput: ElementRef;
  addForm: FormGroup;
  friendModel: FriendModel = {
    id: '',
    name: '',
    email: '',
    phone: '',
    photoUrl: '',
    photoPublicId: '',
    number: '',
    street: '',
    neighborhood: '',
    city: '',
    File: null
  };
  submitted = false;

  public uploader: FileUploader = new FileUploader({});

  constructor(private formBuilder: FormBuilder, 
    private router : Router,
    private friendService: FriendService,
    private alertifyService: AlertifyService,
    private spinner: NgxSpinnerService,
    public imageHelper: ImageHelper) { }

  ngOnInit(): void {
    localStorage.removeItem('image');
    this.initializeForm(); 
    this.setUploaderCallbacks();
  }

  setUploaderCallbacks() {
    this.uploader.onBeforeUploadItem = (item) => {
      item.withCredentials = false;
    }
    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    };
    this.uploader.onCompleteItem = (item: any, response: any, status: any, headers: any) => {
      console.log('FileUpload:uploaded:', item, status, response);
    };
  }

  initializeForm() {
    this.addForm = this.formBuilder.group({
      name: ['', Validators.required],
      email: ['', Validators.required],
      phone: ['', Validators.required],
      number: ['', Validators.required],
      street: ['', Validators.required],
      city: ['', Validators.required],
      neighborhood: ['', Validators.required]
    });
  } 

  selectFile() {
    this.fileUploadInput.nativeElement.click();
  } 

  onSubmit(): void {
    this.submitted = true;
    if (this.addForm.valid) {
      let friendForm = this.addForm.value; 
      const formData = this.populateFormData(friendForm);
      localStorage.setItem('image', friendForm.File);
      this.spinner.show();
      this.friendService.addFriend(formData)
        .subscribe(data => {
          setTimeout(() => {
            this.spinner.hide();
            this.router.navigate(['amigos']);
            this.alertifyService.success("O amigo foi adicionado com sucesso!");
          }, 1000);
        },
          error => {
            console.log(error)
          });
    }
  }

  populateFormData(friendForm: any) {
    const formData = new FormData();
    friendForm.File = this.friendModel.File;
    for (var key in friendForm)
      formData.append(key, friendForm[key]);

    return formData;
  }

  handleFileSelect(evt) {
    const files = evt.target.files;
    const file = files[0];

    if (files && file) {
      this.friendModel.File = file;
      const reader = new FileReader();
      reader.onload = this._handleReaderLoaded.bind(this);
      reader.readAsBinaryString(file);
    }
  }

  _handleReaderLoaded(readerEvt) {
    const binaryString = readerEvt.target.result;
    this.friendModel.photoUrl = 'data:image/jpg;base64,' + btoa(binaryString);
  }
}
