export type estadoPrestamo = 1 | 2 | 3;

export const EstadoPrestamo = {
  pendiente: 1 as estadoPrestamo,
  Aprobado: 2 as estadoPrestamo,
  Rechazado: 3 as estadoPrestamo,
} as const;