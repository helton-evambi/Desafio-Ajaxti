import { CommonModule } from '@angular/common';
import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
} from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { FormInputComponent } from '../../../../shared/components/form-input/form-input.component';
import { FormTextareaComponent } from '../../../../shared/components/form-textarea/form-textarea.component';
import { Author } from '../../models/author.model';

@Component({
  selector: 'app-author-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormInputComponent,
    FormTextareaComponent,
  ],
  templateUrl: './author-form.component.html',
  styleUrl: './author-form.component.scss',
})
export class AuthorFormComponent implements OnInit, OnChanges {
  @Input() author?: Author;
  @Input() saving: boolean = false;

  @Output() formSubmit = new EventEmitter<Author>();
  @Output() formCancel = new EventEmitter<void>();

  authorForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.authorForm = this.createForm();
  }

  ngOnChanges(): void {
    if (this.author && this.authorForm) {
      this.populateForm();
    }
  }

  private createForm(): FormGroup {
    return this.fb.group({
      firstName: ['', [Validators.required, Validators.minLength(2)]],
      lastName: ['', [Validators.required, Validators.minLength(2)]],
      dateOfBirth: [''],
      dateOfDeath: [''],
      biography: [''],
    });
  }

  private populateForm(): void {
    if (this.author) {
      this.authorForm.patchValue({
        firstName: this.author.firstName,
        lastName: this.author.lastName,
        dateOfBirth: this.author.dateOfBirth || '',
        dateOfDeath: this.author.dateOfDeath || '',
        biography: this.author.biography || '',
      });
    }
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.authorForm.get(fieldName);
    return !!(field && field.invalid && (field.dirty || field.touched));
  }

  onSubmit(): void {
    if (this.authorForm.valid && !this.saving) {
      const formValue = this.authorForm.value;

      const authorData: Author = {
        ...formValue,
        id: this.author?.id,
      };

      Object.keys(authorData).forEach((key) => {
        if (
          authorData[key as keyof Author] === '' ||
          authorData[key as keyof Author] === null
        ) {
          delete authorData[key as keyof Author];
        }
      });

      this.formSubmit.emit(authorData);
    } else {
      this.markFormGroupTouched();
    }
  }

  onCancel(): void {
    this.authorForm.reset();
    this.formCancel.emit();
  }

  private markFormGroupTouched(): void {
    Object.keys(this.authorForm.controls).forEach((key) => {
      const control = this.authorForm.get(key);
      control?.markAsTouched();
    });
  }

  resetForm(): void {
    this.authorForm.reset();
  }
  ngOnInit(): void {
    if (this.author) {
      this.populateForm();
    }
  }
}
