'use client';

import { useState, useEffect } from 'react';
import { useRouter } from 'next/navigation';
import {
  Box,
  AppBar,
  Toolbar,
  Typography,
  TextField,
  Button,
  InputAdornment,
  CircularProgress,
  Alert,
  Container,
  Menu,
  MenuItem,
  Divider,
  Paper,
} from '@mui/material';
import {
  Search,
  Logout,
  Person,
  KeyboardArrowDown,
} from '@mui/icons-material';
import { useAuth } from '@/contexts/AuthContext';
import { orderService } from '@/services/api';
import { OrderDetailResponse } from '@/types';
import OrderDetails from '@/components/OrderDetails';

export default function DashboardPage() {
  const { user, isLoading: authLoading, logout } = useAuth();
  const router = useRouter();
  const [searchQuery, setSearchQuery] = useState('');
  const [isSearching, setIsSearching] = useState(false);
  const [error, setError] = useState('');
  const [orderData, setOrderData] = useState<OrderDetailResponse | null>(null);
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);

  useEffect(() => {
    if (!authLoading && !user) {
      router.push('/login');
    }
  }, [user, authLoading, router]);

  const handleSearch = async (e?: React.FormEvent) => {
    e?.preventDefault();
    if (!searchQuery.trim()) return;

    setIsSearching(true);
    setError('');
    setOrderData(null);

    try {
      const data = await orderService.getOrder(searchQuery.trim());
      setOrderData(data);
    } catch (err: any) {
      if (err.response?.status === 404) {
        setError(`No se encontró el pedido "${searchQuery}"`);
      } else {
        setError(err.response?.data?.message || 'Ocurrió un error al buscar');
      }
    } finally {
      setIsSearching(false);
    }
  };

  const handleQuickSearch = async (orderNum: string) => {
    setSearchQuery(orderNum);
    setIsSearching(true);
    setError('');
    setOrderData(null);

    try {
      const data = await orderService.getOrder(orderNum);
      setOrderData(data);
    } catch (err: any) {
      if (err.response?.status === 404) {
        setError(`No se encontró el pedido "${orderNum}"`);
      } else {
        setError(err.response?.data?.message || 'Ocurrió un error al buscar');
      }
    } finally {
      setIsSearching(false);
    }
  };

  const handleLogout = () => {
    logout();
    router.push('/login');
  };

  if (authLoading || !user) {
    return (
      <Box sx={{ minHeight: '100vh', display: 'flex', alignItems: 'center', justifyContent: 'center' }}>
        <CircularProgress />
      </Box>
    );
  }

  return (
    <Box sx={{ minHeight: '100vh', bgcolor: 'background.default' }}>
      <AppBar position="sticky" elevation={0} sx={{ bgcolor: 'primary.main' }}>
        <Toolbar>
          <Box sx={{ display: 'flex', alignItems: 'center', gap: 1.5 }}>
            <Box
              sx={{
                width: 36,
                height: 36,
                borderRadius: 1,
                bgcolor: 'rgba(255,255,255,0.15)',
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center',
              }}
            >
              <Search sx={{ color: 'white' }} />
            </Box>
            <Typography variant="h6" fontWeight={600}>
              Consulta de Pedidos
            </Typography>
          </Box>

          <Box sx={{ flexGrow: 1 }} />

          <Button
            color="inherit"
            onClick={(e) => setAnchorEl(e.currentTarget)}
            endIcon={<KeyboardArrowDown />}
            sx={{ textTransform: 'none' }}
          >
            <Person sx={{ mr: 1 }} />
            {user.fullName}
          </Button>
          <Menu
            anchorEl={anchorEl}
            open={Boolean(anchorEl)}
            onClose={() => setAnchorEl(null)}
          >
            <MenuItem disabled>
              <Typography variant="body2" color="text.secondary">
                {user.role === 'CustomerService' ? 'Atención al Cliente' : user.role}
              </Typography>
            </MenuItem>
            <Divider />
            <MenuItem onClick={handleLogout}>
              <Logout sx={{ mr: 1, fontSize: 20 }} />
              Cerrar Sesión
            </MenuItem>
          </Menu>
        </Toolbar>
      </AppBar>

      <Paper 
        elevation={0} 
        sx={{ 
          borderBottom: '1px solid',
          borderColor: 'divider',
          bgcolor: 'white',
        }}
      >
        <Container maxWidth="lg" sx={{ py: 3 }}>
          <form onSubmit={handleSearch}>
            <Box sx={{ display: 'flex', gap: 2, alignItems: 'flex-start' }}>
              <TextField
                fullWidth
                placeholder="Ingresa el número de pedido (ej: ORD-2024-78542)"
                value={searchQuery}
                onChange={(e) => setSearchQuery(e.target.value)}
                InputProps={{
                  startAdornment: (
                    <InputAdornment position="start">
                      <Search color="action" />
                    </InputAdornment>
                  ),
                }}
                sx={{ maxWidth: 500 }}
              />
              <Button
                type="submit"
                variant="contained"
                size="large"
                disabled={isSearching || !searchQuery.trim()}
                sx={{ px: 4, height: 56 }}
              >
                {isSearching ? <CircularProgress size={24} color="inherit" /> : 'Buscar'}
              </Button>
            </Box>
          </form>

          <Box sx={{ mt: 2 }}>
            <Typography variant="caption" color="text.secondary" sx={{ mr: 1 }}>
              Pedidos de ejemplo:
            </Typography>
            {['ORD-2024-78542', 'ORD-2024-79123', 'ORD-2024-79456'].map((orderNum) => (
              <Button
                key={orderNum}
                size="small"
                onClick={() => handleQuickSearch(orderNum)}
                sx={{ 
                  mr: 1, 
                  textTransform: 'none',
                  color: 'text.secondary',
                  '&:hover': { bgcolor: 'grey.100' }
                }}
              >
                {orderNum}
              </Button>
            ))}
          </Box>
        </Container>
      </Paper>

      <Container maxWidth="lg" sx={{ py: 4 }}>
        {error && (
          <Alert severity="error" sx={{ mb: 3 }}>
            {error}
          </Alert>
        )}

        {isSearching && (
          <Box sx={{ display: 'flex', justifyContent: 'center', py: 8 }}>
            <CircularProgress />
          </Box>
        )}

        {!isSearching && !orderData && !error && (
          <Box sx={{ textAlign: 'center', py: 8 }}>
            <Search sx={{ fontSize: 64, color: 'grey.300', mb: 2 }} />
            <Typography variant="h6" color="text.secondary">
              Ingresa un número de pedido para comenzar
            </Typography>
            <Typography variant="body2" color="text.secondary" sx={{ mt: 1 }}>
              Busca cualquier pedido para ver datos del cliente, productos, estado de envío y más
            </Typography>
          </Box>
        )}

        {orderData && <OrderDetails data={orderData} />}
      </Container>
    </Box>
  );
}