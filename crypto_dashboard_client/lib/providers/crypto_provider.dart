import 'package:flutter/material.dart';
import '../models/crypto_pair.dart';
import '../services/api_service.dart';

class CryptoProvider with ChangeNotifier {
  List<CryptoPair> _cryptoPairs = [];
  bool _isLoading = false;

  List<CryptoPair> get cryptoPairs => _cryptoPairs;
  bool get isLoading => _isLoading;

  void fetchCryptoPairs(DateTime startDate, DateTime endDate) async {
    _isLoading = true;
    notifyListeners();

    _cryptoPairs = await ApiService().getCryptoPairs(startDate, endDate);

    _isLoading = false;
    notifyListeners();
  }

  void fetchDefaultCryptoPairs() async {
    _isLoading = true;
    notifyListeners();

    _cryptoPairs = await ApiService().getDefaultCryptoPairs();

    _isLoading = false;
    notifyListeners();
  }
}
