export type JwtPayload = {
  Userid: string;
  Email: string;
  role: string;
  exp?: number;
};