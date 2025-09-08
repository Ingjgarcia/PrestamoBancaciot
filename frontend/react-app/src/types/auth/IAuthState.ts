export interface IAuthState {
  email: string | null;
  token: string | null;
  rol: string | null;
  userId: string | null;
  status: "idle" | "loading" | "succeeded" | "failed";
  error?: string | null;
}