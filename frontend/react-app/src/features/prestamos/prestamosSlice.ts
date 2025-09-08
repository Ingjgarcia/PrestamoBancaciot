import { createSlice } from "@reduxjs/toolkit";
import {
  addPrestamo,
  getPrestamosAll,
  getPrestamosPendientes,
  aprobarPrestamo,
  rechazarPrestamo,
} from "./api";
import type { IPrestamosState } from "../../types/Prestamos/IPrestamosState";

const initialState: IPrestamosState = {
  items: [],
  pendientes: [],
  selected: null,
  status: "idle",
  error: null,
};

const prestamosSlice = createSlice({
  name: "prestamos",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      // Add
      .addCase(addPrestamo.pending, (s) => { s.status = "loading"; s.error = null; })
      .addCase(addPrestamo.fulfilled, (s, a) => { s.status = "succeeded"; s.items.push(a.payload); })
      .addCase(addPrestamo.rejected, (s, a) => { s.status = "failed"; s.error = a.error.message || "Error al crear"; })

      // Get all
      .addCase(getPrestamosAll.pending, (s) => { s.status = "loading"; s.error = null; })
      .addCase(getPrestamosAll.fulfilled, (s, a) => { s.status = "succeeded"; s.items = a.payload; })
      .addCase(getPrestamosAll.rejected, (s, a) => { s.status = "failed"; s.error = a.error.message || "Error"; })

      // Get pendientes
      .addCase(getPrestamosPendientes.fulfilled, (s, a) => { s.pendientes = a.payload; })

      // Aprobar/Rechazar
      .addCase(aprobarPrestamo.fulfilled, (s, a) => {
        s.items = s.items.map(p => p.id === a.payload.id ? a.payload : p);
        s.pendientes = s.pendientes.filter(p => p.id !== a.payload.id);
      })
      .addCase(rechazarPrestamo.fulfilled, (s, a) => {
        s.items = s.items.map(p => p.id === a.payload.id ? a.payload : p);
        s.pendientes = s.pendientes.filter(p => p.id !== a.payload.id);
      });
  },
});

export default prestamosSlice.reducer;