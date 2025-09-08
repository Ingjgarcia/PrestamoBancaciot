import { useEffect, useState, type JSX } from "react";
import { useAppDispatch, useAppSelector } from "../../app/hooks";
import { Card } from "../../components/Card";
import { login, register } from "./api";
import Spinner from "../../components/Spinner";
import { useNavigate } from "react-router-dom";


function AuthView(): JSX.Element {
    const dispatch = useAppDispatch();
    const { status, error, token } = useAppSelector((s) => s.auth);
    const [mode, setMode] = useState<"login" | "register">("login");
    const [email, setEmail] = useState("");
    const [constrasena, setContrasena] = useState("");
    const navigate = useNavigate();

    const submit = (e: React.FormEvent) => {
        e.preventDefault();
        if (mode === "login") dispatch(login({ email, constrasena }));
        else dispatch(register({ email, constrasena }));
    };

useEffect(() => {
    if (token) {
      navigate("/dashboard", { replace: true });
    }
  }, [token, navigate]);
  
    return (
        <div className="min-h-screen grid place-items-center bg-gradient-to-br from-indigo-50 to-sky-50 p-4">
            <Card className="w-full max-w-md">
                <div className="flex items-center justify-between mb-6">
                    <h1 className="text-2xl font-bold">Gestión de Préstamos</h1>
                    <div className="text-sm">
                        <button
                            onClick={() => setMode(mode === "login" ? "register" : "login")}
                            className="text-indigo-600 hover:underline"
                        >
                            {mode === "login" ? "Crear cuenta" : "Ya tengo cuenta"}
                        </button>
                    </div>
                </div>
                <form className="space-y-4" onSubmit={submit}>
                    <div>
                        <label className="block text-sm font-medium">Email</label>
                        <input
                            type="email"
                            required
                            className="mt-1 w-full rounded-xl border px-3 py-2 focus:outline-none focus:ring-2 focus:ring-indigo-200"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                        />
                    </div>
                    <div>
                        <label className="block text-sm font-medium">Contraseña</label>
                        <input
                            type="password"
                            required
                            className="mt-1 w-full rounded-xl border px-3 py-2 focus:outline-none focus:ring-2 focus:ring-indigo-200"
                            value={constrasena}
                            onChange={(e) => setContrasena(e.target.value)}
                        />
                    </div>
                    {error && <p className="text-sm text-red-600">{error}</p>}
                    <button
                        type="submit"
                        disabled={status === "loading"}
                        className="w-full rounded-xl bg-indigo-600 text-white py-2 font-medium hover:bg-indigo-700 disabled:opacity-60 flex items-center justify-center gap-2"
                    >
                        {status === "loading" && <Spinner />} {mode === "login" ? "Iniciar sesión" : "Registrarme"}
                    </button>
                    <p className="text-xs text-gray-500">* By José García</p>
                </form>
            </Card>
        </div>
    );
}
export default AuthView;