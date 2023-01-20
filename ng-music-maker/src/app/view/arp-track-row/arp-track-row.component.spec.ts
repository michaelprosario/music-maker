import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ArpTrackRowComponent } from './arp-track-row.component';

describe('ArpTrackRowComponent', () => {
  let component: ArpTrackRowComponent;
  let fixture: ComponentFixture<ArpTrackRowComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ArpTrackRowComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ArpTrackRowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
