const variants = {
  primary: 'bg-forest text-bone hover:bg-forest-dark',
  danger: 'bg-red-700 text-white hover:bg-red-800',
  secondary: 'bg-white text-charcoal border border-charcoal/15 hover:bg-charcoal/5',
  gold: 'bg-gold text-forest-dark hover:bg-gold-light',
}

function Button({ children, variant = 'primary', className = '', ...props }) {
  return (
    <button
      className={`px-4 py-2 rounded-lg font-medium text-sm transition-colors ${variants[variant]} ${className}`}
      {...props}
    >
      {children}
    </button>
  )
}

export default Button