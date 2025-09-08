export type Prestamo = {
  id: string;
  usuario: string;
  cantidad: number;
  tiempo: number;
  estado?: number;
  fechaCreacion?: string;
  fechaModificacion?: string;
  usuarioModificacion?: string;
};