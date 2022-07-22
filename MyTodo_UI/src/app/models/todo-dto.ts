import { baseDto } from "./base-dto";

export interface TodoDto extends baseDto
{
  title: string,
  description: string,
  expiration?: Date,
  userId: number,
}
