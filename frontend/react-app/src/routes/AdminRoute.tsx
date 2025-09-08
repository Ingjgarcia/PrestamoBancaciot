import { Navigate } from "react-router-dom";
import { useAppSelector } from "../app/hooks";
import type { JSX } from "react";

export default function AdminRoute({ children }: { children: JSX.Element }) {
  const { token, rol } = useAppSelector((s) => s.auth);
  if (!token) return <Navigate to="/login" replace />;
  if (rol?.toLowerCase() !== "admin") return <Navigate to="/dashboard" replace />;
  return children;
}