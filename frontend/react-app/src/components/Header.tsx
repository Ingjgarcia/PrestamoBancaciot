import { useAppDispatch, useAppSelector } from "../app/hooks";
import { logout } from "../features/auth/authSlice";
import Badge from "./Badge";

export default function Header() {
  const dispatch = useAppDispatch();
  const { email, rol } = useAppSelector((s) => s.auth);

  return (
    <header className="bg-white border-b px-6 py-3 flex items-center justify-between shadow-sm">
      {/* Título o Branding */}
      <div className="flex items-center gap-3">
        <span className="text-lg font-bold text-indigo-700">Gestión de Préstamos</span>
        {rol && <Badge color="yellow">{rol}</Badge>}
      </div>

      {/* Usuario */}
      <div className="flex items-center gap-4">
        <span className="text-sm text-gray-700">{email}</span>
        <button
          onClick={() => dispatch(logout())}
          className="px-3 py-1 rounded-lg bg-red-500 text-white text-sm font-medium hover:bg-red-600"
        >
          Cerrar sesión
        </button>
      </div>
    </header>
  );
}