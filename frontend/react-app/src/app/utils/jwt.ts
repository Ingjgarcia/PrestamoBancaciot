import { jwtDecode } from "jwt-decode";
import type { JwtPayload } from "../../types/JwtPayload";

export function decodeToken(token: string): JwtPayload {
  return jwtDecode<JwtPayload>(token);
}
