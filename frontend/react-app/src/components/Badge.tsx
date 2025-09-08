export default function Badge({ children, color }: { children: React.ReactNode; color?: "green" | "red" | "yellow" }) {
  const map: Record<string, string> = {
    green: "bg-green-100 text-green-700",
    red: "bg-red-100 text-red-700",
    yellow: "bg-yellow-100 text-yellow-700",
  };

  return (
    <span className={`px-2 py-1 rounded-full text-xs font-semibold ${map[color || "yellow"]}`}>
      {children}
    </span>
  );
}