import axios from 'axios';
import { LoginRequest, LoginResponse, OrderDetailResponse } from '@/types';

const API_URL = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5000/api';

const api = axios.create({
  baseURL: API_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

api.interceptors.request.use((config) => {
  if (typeof window !== 'undefined') {
    const token = localStorage.getItem('token');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
  }
  return config;
});

api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401 && typeof window !== 'undefined') {
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      window.location.href = '/login';
    }
    return Promise.reject(error);
  }
);

export const authService = {
  login: async (data: LoginRequest): Promise<LoginResponse> => {
    const response = await api.post<LoginResponse>('/auth/login', data);
    return response.data;
  },
};

export const orderService = {
  getOrder: async (orderNumber: string): Promise<OrderDetailResponse> => {
    const response = await api.get<OrderDetailResponse>(`/orders/${encodeURIComponent(orderNumber)}`);
    return response.data;
  },
  searchOrder: async (orderNumber: string): Promise<OrderDetailResponse> => {
    const response = await api.post<OrderDetailResponse>('/orders/search', { orderNumber });
    return response.data;
  },
};

export default api;
