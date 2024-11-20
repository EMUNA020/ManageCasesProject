import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrl: './search.component.css'
})
export class SearchComponent {
  @Output() filterChanged = new EventEmitter<{
    search: string;
    status: string;
    myFiles: boolean;
  }>();
  @Output() sortChanged = new EventEmitter<'openingDate' | 'fileNumber'>();

  private filter = {
    search: '',
    status: 'Everything',
    myFiles: false,
  };

  onSearch(event: Event) {
    const value = (event.target as HTMLInputElement).value;
    this.filter.search = value;
    this.filterChanged.emit(this.filter);
  }
  onStatusChange(event: Event) {
    const value = (event.target as HTMLSelectElement).value;
    this.filter.status = value;
    this.filterChanged.emit(this.filter);
  }

  onMyFilesChange(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.filter.myFiles = isChecked;
    this.filterChanged.emit(this.filter);
  }

  onSort(sortField: 'openingDate' | 'fileNumber') {
    this.sortChanged.emit(sortField);
  }
}
