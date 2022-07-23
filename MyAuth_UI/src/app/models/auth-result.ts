export interface AuthResult {
  token: string,
  expiration?: Date,
  name: string,
  userId: number
}
