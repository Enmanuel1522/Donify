const colorMap = {
  green: 'bg-forest/10 text-forest border-forest/20',
  gold: 'bg-gold/15 text-gold-light border-gold/30 !text-amber-800',
  red: 'bg-red-50 text-red-700 border-red-200',
  gray: 'bg-charcoal/5 text-charcoal/60 border-charcoal/10',
}

function Badge({ children, color = 'gray' }) {
  return (
    <span
      className={`inline-flex items-center px-2.5 py-1 rounded-full text-xs font-medium border ${colorMap[color]}`}
    >
      {children}
    </span>
  )
}

export default Badge
