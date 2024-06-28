import 'dart:convert';
import 'package:http/http.dart' as http;
import '../models/crypto_pair.dart';

class ApiService {
  static const String baseUrl = 'http://localhost:5000';

  Future<List<CryptoPair>> getCryptoPairs(DateTime startDate, DateTime endDate) async {
    final response = await http.get(
      Uri.parse('$baseUrl/api/highest-average-difference?startDate=${startDate.toIso8601String()}&endDate=${endDate.toIso8601String()}'),
    );

    if (response.statusCode == 200) {
      List<dynamic> body = jsonDecode(response.body);
      return body.map((dynamic item) => CryptoPair.fromJson(item)).toList();
    } else {
      throw Exception('Failed to load crypto pairs');
    }
  }

  Future<List<CryptoPair>> getDefaultCryptoPairs() async {
    final response = await http.get(
      Uri.parse('$baseUrl/api/default-pairs'),
    );

    if (response.statusCode == 200) {
      List<dynamic> body = jsonDecode(response.body);
      return body.map((dynamic item) => CryptoPair.fromJson(item)).toList();
    } else {
      throw Exception('Failed to load default crypto pairs');
    }
  }
}
