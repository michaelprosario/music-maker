import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ArpTrackCellComponent } from './arp-track-cell.component';

describe('ArpTrackCellComponent', () => {
  let component: ArpTrackCellComponent;
  let fixture: ComponentFixture<ArpTrackCellComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ArpTrackCellComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ArpTrackCellComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
