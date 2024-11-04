import { ResolveFn } from '@angular/router';
import { Member } from '../_models/member';
import { inject } from '@angular/core';
import { MembersService } from '../_services/members.service';
import { count } from 'rxjs';
import { ReturnStatement } from '@angular/compiler';

export const memberDetailedResolver: ResolveFn<Member | null> = (route, state) => {
  const memberService = inject(MembersService);
  const username = route.paramMap.get('username');

  if(!username) return null;

  return memberService.getMember(username);
};
