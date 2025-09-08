import type { Prestamo } from "./Prestamo";

export interface IPrestamosState {
  items: Prestamo[];
  pendientes: Prestamo[];
  selected?: Prestamo | null;
  status: "idle" | "loading" | "succeeded" | "failed";
  error?: string | null;
}